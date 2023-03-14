function showCategories(equipe, categorie) {
    $.ajax({
        url: "/Product/GetCategories",
        type: "get",
        data: { equipe: equipe, categorie: categorie },
        success: function (data) {
            var container = $(`#categories-${equipe}`);

            // clear any previous content
            container.empty();

            // add the subcategories for this category
            $.each(data, function (index, sousCategorie) {
                container.append(`<button onclick="showSousCategories('${equipe}', '${categorie}', '${sousCategorie.Name}')">Sous-catégorie : ${sousCategorie.Name}</button>`);
            });
        }
    });
}

function showSousCategories(equipe, categorie, sousCategorie) {
    $.ajax({
        url: "/Product/GetSousCategories",
        type: "get",
        data: { equipe: equipe, categorie: categorie, sousCategorie: sousCategorie },
        success: function (data) {
            var container = $(`#sous-categories-${equipe}-${categorie}`);

            // clear any previous content
            container.empty();

            // add the products for this sous-category
            $.each(data, function (index, produit) {
                container.append(`
                        <div>
                            <h4>Produit : ${produit.Produit}</h4>
                            <p>Taille : ${produit.Taille}</p>
                            <p>Quantité : ${produit.Quantite}</p>
                            <p>Prix : ${produit.Prix}</p>
                            <p>Fournisseur : ${produit.Fournisseur}</p>
                            <form asp-controller="Cart" asp-action="Add" method="post">
                                <input type="hidden" name="id" value="${produit.Id}" />
                                <input type="hidden" name="equipe" value="${equipe}" />
                                <input type="hidden" name="categorie" value="${categorie}" />
                                <input type="hidden" name="sousCategorie" value="${sousCategorie}" />
                                <button type="submit">Ajouter au panier</button>
                            </form>
                        </div>
                    `);
            });
        }
    });
}