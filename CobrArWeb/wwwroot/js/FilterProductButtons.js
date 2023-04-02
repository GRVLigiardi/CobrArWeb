function init() {
    let mySelectedTeamId = null;
    let mySelectedCategoryId = 'clear';

    function filterRows() {
        let rows = document.querySelectorAll('#product-table tbody tr');
        rows.forEach(function (row) {
            let shouldDisplay = (row.dataset.teamId === mySelectedTeamId || mySelectedTeamId === 'clear') &&
                (row.dataset.categoryId === mySelectedCategoryId || mySelectedCategoryId === 'clear');
            // Vérifiez si une équipe et une catégorie sont sélectionnées
            if (mySelectedTeamId !== 'clear' && mySelectedCategoryId !== 'clear') {
                row.style.display = shouldDisplay ? '' : 'none';
            } else {
                row.style.display = 'none';
            }
        });
    }

    document.querySelectorAll('.team-buttons button').forEach(function (button) {
        button.addEventListener('click', function () {
            mySelectedTeamId = this.dataset.teamId;
            filterRows();
        });
    });

    document.querySelectorAll('.cat-buttons button').forEach(function (button) {
        button.addEventListener('click', function () {
            mySelectedCategoryId = this.dataset.categoryId;
            updateSubCategories(mySelectedCategoryId);
            filterRows();
        });
    });

    function updateSubCategories(categoryId) {
        let rows = document.querySelectorAll('#product-table tbody tr');
        let subCatSelect = document.querySelector('.subcat-buttons select');

        // Clear the select element
        subCatSelect.innerHTML = '<option value="">Toutes les sous-catégories</option>';

        // Loop through the products and add the subcategories to the select element
        rows.forEach(function (row) {
            if (row.dataset.categoryId === categoryId || categoryId === 'clear') {
                let subCatId = row.dataset.subCategoryId;
                let subCatName = row.dataset.subCategoryName;

                // Check if the sub-category is already in the select element
                let existingSubCat = subCatSelect.querySelector(`[value="${subCatId}"]`);
                if (!existingSubCat) {
                    let subCatOption = document.createElement('option');
                    subCatOption.value = subCatId;
                    subCatOption.textContent = subCatName;
                    subCatSelect.appendChild(subCatOption);
                }
            }
        });
    }

    document.querySelector('.subcat-buttons select').addEventListener('change', function () {
        let mySelectedSubCategoryId = this.value;
        let rows = document.querySelectorAll('#product-table tbody tr');
        rows.forEach(function (row) {
            if (row.dataset.subCategoryId === mySelectedSubCategoryId || mySelectedSubCategoryId === '') {
                row.style.display = (row.dataset.teamId === mySelectedTeamId || mySelectedTeamId === 'clear') ? '' : 'none';
            } else {
                row.style.display = 'none';
            }
        });
    });
    // Désactivez initialement les boutons de catégorie
    let catButtons = document.querySelectorAll('.cat-buttons button');
    catButtons.forEach(function (button) {
        button.disabled = true;
    });

    // Activez les boutons de catégorie lorsque l'utilisateur choisit une équipe
    document.querySelectorAll('.team-buttons button').forEach(function (button) {
        button.addEventListener('click', function () {
            mySelectedTeamId = this.dataset.teamId;
            catButtons.forEach(function (catButton) {
                catButton.disabled = false;
            });
            // Réinitialiser la sélection de la catégorie et cacher tous les produits
            mySelectedCategoryId = 'clear';
            filterRows();
        });
    });

    // Lorsqu'une catégorie est sélectionnée, affichez les produits correspondants
    document.querySelectorAll('.cat-buttons button').forEach(function (button) {
        button.addEventListener('click', function () {
            mySelectedCategoryId = this.dataset.categoryId;
            updateSubCategories(mySelectedCategoryId);
            filterRows();
        });
    });

    // Cachez initialement tous les produits
    filterRows();
}


document.addEventListener('DOMContentLoaded', init);