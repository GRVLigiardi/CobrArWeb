using CobrArWeb.Data;
using CobrArWeb.Models;
using CobrArWeb.Models.RechercheArbo;
using CobrArWeb.Models.Statistiques;
using CobrArWeb.Models.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CobrArWeb.Controllers
{
    public class StockController : Controller
    {
        private readonly CobrArWebContext _context;

        public Product NewProduct { get; set; } = new Product();

        public StockController(CobrArWebContext context)
        {
            _context = context;
        }

        private List<EquipeViewModel> GetEquipeViewModelList()
        {
            var equipes = _context.Products
                .Include(p => p.Equipe)
                .Include(p => p.Categorie)
                .Include(p => p.SousCategorie)
                .Include(p => p.Taille)
                .Include(p => p.Fournisseur)
                .GroupBy(p => p.Equipe.Nom)
                .Select(g => new EquipeViewModel
                {
                    Equipe = g.Key,
                    Categorie = g.ToList().GroupBy(p => p.Categorie.Nom).Select(gc => new CategoryViewModel { Categorie = gc.Key, SousCategorie = SousCategorieViewModel.Clean(gc.ToList(), new EquipeViewModel { Equipe = g.Key }) }).ToList(),
                }).ToList();

            return equipes;
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var equipes = _context.Equipes.ToList();
            var categories = _context.Categories.ToList();
            var sousCategories = _context.SousCategories.ToList();
            var tailles = _context.Tailles.ToList();
            var fournisseurs = _context.Fournisseurs.ToList();
            var viewModel = new StockViewModel

            {
                NewProduct = new Product(),
                Products = products,
                Equipes = equipes,
                Categories = categories,
                SousCategories = sousCategories,
                Tailles = tailles,
                Fournisseurs = _context.Fournisseurs.ToList(),
                ListViewModel = new ListViewModel
                {
                    EquipeViewModelList = GetEquipeViewModelList()
                }

            };
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                ViewData["IsAdmin"] = true;
            }
            SetViewBagDropdownLists();
            ViewBag.IsStockView = true;
            return View("Stock", viewModel);
        }

        public void SetViewBagDropdownLists(int? selectedTailleId = null, int? selectedCategorieId = null)
        {

            ViewBag.TailleList = new SelectList(_context.Tailles, "Id", "Nom", selectedTailleId);


            ViewBag.CategorieList = new SelectList(_context.Categories, "Id", "Nom");


            ViewBag.SousCategorieList = new SelectList(_context.SousCategories.Where(sc => sc.CategorieId == selectedCategorieId), "Id", "Nom");


            ViewBag.EquipeList = new SelectList(_context.Equipes, "Id", "Nom");


            ViewBag.FournisseurList = new SelectList(_context.Fournisseurs, "Id", "Nom");
        }


        [HttpGet]
        public JsonResult GetSousCategoriesByCategorie(int categorieId)
        {
            var sousCategories = _context.SousCategories
                .Where(sc => sc.CategorieId == categorieId)
                .Select(sc => new { sc.Id, sc.Nom })
                .ToList();

            Console.WriteLine($"Sous-categories for categorieId {categorieId}: {sousCategories.Count}");
            foreach (var sc in sousCategories)
            {
                Console.WriteLine($" - {sc.Id}: {sc.Nom}");
            }

            return Json(sousCategories);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Include(p => p.Categorie).Include(p => p.SousCategorie).Include(p => p.Equipe).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            SetViewBagDropdownLists(product.TailleId, product.CategorieId);
            return View("EditProduct", product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product, bool applyToAllSizes = false, bool applyToAllTeams = false, bool applyToAllSuppliers = false)
        {
            // Vérifier si l'ID du produit existe déjà dans la base de données
            var existingProduct = _context.Products.Find(product.Id);
            UpdateOrCreateProducts(product, "UPDATE", applyToAllSizes, applyToAllTeams, applyToAllSuppliers);
            if (existingProduct != null)
            {
                // Mettre à jour le produit existant dans la base de données
                var oldProduct = _context.Products.AsNoTracking().First(p => p.Id == product.Id);
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                _context.SaveChanges();

                // Ajouter l'historique de modification du produit
                AddProductHistory(oldProduct, product, "UPDATE");

                // Ajouter le message à TempData
                TempData["message"] = $"Le produit '{product.Produit}' de l'équipe , de la taille '{_context.Tailles.Find(product.TailleId).Nom}', de prix '{product.Prix}' et de quantité '{product.Quantite}' Modifé avec succès. ";
            }
            else
            {
                // Ajouter le nouveau produit à la base de données
                _context.Products.Add(product);
                _context.SaveChanges();

                // Ajouter l'historique de création du produit
                AddProductHistory(null, product, "CREATE");
            }

            // Retourner à la vue "EditProduct.cshtml" après l'édition
            SetViewBagDropdownLists(product.TailleId, product.CategorieId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategorieList = new SelectList(_context.Categories, "Id", "Nom");
            ViewBag.AllSubCategories = _context.SousCategories.ToList();
            SetViewBagDropdownLists();
            return View("CreateProduct", new Product());
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product, bool applyToAllSizes = false, bool applyToAllTeams = false, bool applyToAllSuppliers = false)
        {
            // Ajouter le nouveau produit à la base de données
            _context.Products.Add(product);
            _context.SaveChanges();

            // Ajouter l'historique de création du produit
            AddProductHistory(null, product, "CREATE");
            UpdateOrCreateProducts(product, "CREATE", applyToAllSizes, applyToAllTeams, applyToAllSuppliers);
            TempData["message"] = $"Le produit '{product.Produit}' de l'équipe '{_context.Equipes.Find(product.EquipeId).Nom}', de la catégorie '{_context.Categories.Find(product.CategorieId).Nom}', de la sous-catégorie '{_context.SousCategories.Find(product.SousCategorieId).Nom}', de la taille '{_context.Tailles.Find(product.TailleId).Nom}', de prix '{product.Prix}' et de quantité '{product.Quantite}' du fournisseur '{_context.Fournisseurs.Find(product.FournisseurId).Nom}' a été ajouté avec succès.";

            // Retourner à la vue "CreateProduct.cshtml" après la création
            ViewBag.CategorieList = new SelectList(_context.Categories, "Id", "Nom");
            ViewBag.AllSubCategories = _context.SousCategories.ToList();
            SetViewBagDropdownLists();
            return RedirectToAction("Index");
        }

        private void AddProductHistory(Product oldProduct, Product newProduct, string action)
        {
            var properties = typeof(Product).GetProperties();
            foreach (var property in properties)
            {
                bool valueChanged;

                if (action == "CREATE")
                {
                    valueChanged = newProduct != null && property.GetValue(newProduct) != null;
                }
                else
                {
                    valueChanged = (oldProduct != null && property.GetValue(oldProduct) != null) != (newProduct != null && property.GetValue(newProduct) != null);
                }

                if (valueChanged)
                {
                    _context.ProductHistories.Add(new ProductHistory
                    {
                        ProductId = newProduct.Id,
                        Action = action,
                        ColumnModified = property.Name,
                        OldValue = oldProduct != null && property.GetValue(oldProduct) != null ? property.GetValue(oldProduct).ToString() : null,
                        NewValue = newProduct != null && property.GetValue(newProduct) != null ? property.GetValue(newProduct).ToString() : null,
                        Date = DateTime.UtcNow
                    });
                }
            }
            _context.SaveChanges();
        }
        public IActionResult GetAllProductHistories()
        {
            var groupedProductHistories = _context.ProductHistories
                        .GroupBy(ph => ph.ProductId)
                        .Select(group => new ProductHistoryGroupViewModel
                        {
                            ProductId = group.Key,
                            Histories = group.Select(ph => new ProductHistoryViewModel
                            {
                                Id = ph.Id,
                                Action = ph.Action,
                                ColumnModified = ph.ColumnModified,
                                OldValue = ph.OldValue,
                                NewValue = ph.NewValue,
                                Date = ph.Date
                            }).ToList()
                        });

            return PartialView("_ProductHistories", groupedProductHistories.ToList());
        }

        private void UpdateOrCreateProducts(Product product, string action, bool applyToAllSizes, bool applyToAllTeams, bool applyToAllSuppliers)
        {
            // Récupérer les produits concernés en fonction des options sélectionnées
            var productsToUpdate = _context.Products
                .Where(p => p.CategorieId == product.CategorieId && p.SousCategorieId == product.SousCategorieId)
                .AsQueryable();

            var allTeams = _context.Equipes.ToList();

            if (!applyToAllSizes)
            {
                productsToUpdate = productsToUpdate.Where(p => p.TailleId == product.TailleId);
            }

            if (!applyToAllTeams)
            {
                productsToUpdate = productsToUpdate.Where(p => p.EquipeId == product.EquipeId);
            }

            if (!applyToAllSuppliers)
            {
                productsToUpdate = productsToUpdate.Where(p => p.FournisseurId == product.FournisseurId);
            }

            // Mettre à jour ou créer les produits concernés
            foreach (var prodToUpdate in productsToUpdate)
            {
                if (_context.Tailles.Find(prodToUpdate.TailleId) != null)
                {
                    if (action == "UPDATE")
                    {
                        // Mettre à jour les champs du produit
                        var oldProduct = _context.Products.AsNoTracking().First(p => p.Id == prodToUpdate.Id);
                        var updatedProduct = new Product
                        {
                            Id = prodToUpdate.Id,
                            CategorieId = product.CategorieId,
                            SousCategorieId = product.SousCategorieId,
                            EquipeId = applyToAllTeams ? prodToUpdate.EquipeId : product.EquipeId,
                            TailleId = applyToAllSizes ? prodToUpdate.TailleId : product.TailleId,
                            FournisseurId = applyToAllSuppliers ? prodToUpdate.FournisseurId : product.FournisseurId,
                            Produit = product.Produit,
                            Prix = product.Prix,
                            Quantite = product.Quantite
                        };

                        _context.Entry(prodToUpdate).CurrentValues.SetValues(updatedProduct);
                        _context.SaveChanges();
                        AddProductHistory(oldProduct, updatedProduct, "UPDATE");
                    }
                }
                else if (action == "CREATE")
                {
                    var allSizes = _context.Tailles.Where(t => t.Id != 1002).ToList(); // Exclure la taille avec Id == 1002

                    if (applyToAllTeams)
                    {
                        foreach (var team in allTeams)
                        {
                            foreach (var size in allSizes) // Ajouter cette boucle pour créer un produit pour chaque taille
                            {
                                var newProduct = new Product
                                {
                                    CategorieId = product.CategorieId,
                                    SousCategorieId = product.SousCategorieId,
                                    EquipeId = team.Id,
                                    TailleId = size.Id, // Utiliser l'ID de la taille en cours de la boucle
                                    FournisseurId = applyToAllSuppliers ? prodToUpdate.FournisseurId : product.FournisseurId,
                                    Produit = product.Produit,
                                    Prix = product.Prix,
                                    Quantite = product.Quantite
                                };

                                _context.Products.Add(newProduct);
                                _context.SaveChanges();

                                // Ajouter l'historique de création du produit
                                AddProductHistory(null, newProduct, "CREATE");
                            }
                        }
                    }
                    else
                    {
                        foreach (var size in allSizes) // Ajouter cette boucle pour créer un produit pour chaque taille
                        {
                            var newProduct = new Product
                            {
                                CategorieId = product.CategorieId,
                                SousCategorieId = product.SousCategorieId,
                                EquipeId = product.EquipeId,
                                TailleId = size.Id, // Utiliser l'ID de la taille en cours de la boucle
                                FournisseurId = applyToAllSuppliers ? prodToUpdate.FournisseurId : product.FournisseurId,
                                Produit = product.Produit,
                                Prix = product.Prix,
                                Quantite = product.Quantite
                            };

                            _context.Products.Add(newProduct);
                            _context.SaveChanges();

                            // Ajouter l'historique de création du produit
                            AddProductHistory(null, newProduct, "CREATE");
                        }
                        }
                }
            }
        }
    }
}