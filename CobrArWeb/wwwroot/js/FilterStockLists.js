document.addEventListener('DOMContentLoaded', function () {
    const categoryDropdown = document.getElementById('categorie-dropdown');
    const subCategoryDropdown = document.getElementById('souscategorie-dropdown');

    if (categoryDropdown && subCategoryDropdown) {
        function updateSubCategories(categoryId) {
            const allSubCategories = JSON.parse(document.getElementById('all-subcategories').textContent);
            console.log(allSubCategories);
            const allSubCategoriesArray = Array.from(allSubCategories.$values);
            const filteredSubCategories = allSubCategoriesArray.filter(subCategory => subCategory.categorieId == categoryId);

            subCategoryDropdown.innerHTML = '<option value="">Sélectionnez une sous-catégorie</option>';
            filteredSubCategories.forEach(subCategory => {
                const option = document.createElement('option');
                option.value = subCategory.id;
                option.textContent = subCategory.nom;
                subCategoryDropdown.appendChild(option);
            });
        }

        categoryDropdown.addEventListener('change', function () {
            updateSubCategories(this.value);
        });
    }
});