document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('product-table');
    const bocaButton = document.getElementById('boca-button');
    const riverButton = document.getElementById('river-button');
    const centralButton = document.getElementById('central-button');
    const newellsButton = document.getElementById('newells-button');
    const sanlorenzoButton = document.getElementById('sanlorenzo-button');
    const independienteButton = document.getElementById('independiente-button');
    const racingButton = document.getElementById('racing-button');
    const afaButton = document.getElementById('afa-button');
    const otrosButton = document.getElementById('otros-button');
    const clearButton = document.getElementById('clear-button');






    bocaButton.addEventListener('click', () => filterTableByCategory('BOJ Boca Juniors'));
    riverButton.addEventListener('click', () => filterTableByCategory('RPL River Plate'));
    centralButton.addEventListener('click', () => filterTableByCategory('RCT Rosario Central'));
    newellsButton.addEventListener('click', () => filterTableByCategory('NOB Newells Old Boys'));
    sanlorenzoButton.addEventListener('click', () => filterTableByCategory('SLO San Lorenzo'));
    independienteButton.addEventListener('click', () => filterTableByCategory('CAI Independiente'));
    racingButton.addEventListener('click', () => filterTableByCategory('RAC Racing'));
    afaButton.addEventListener('click', () => filterTableByCategory('AFA Selección Argentina'));
    otrosButton.addEventListener('click', () => filterTableByCategory('OTR Otros'));
    clearButton.addEventListener('click', clearFilter); 


    function filterTableByCategory(category) {
        const rows = table.getElementsByTagName('tr');
        for (let i = 0; i < rows.length; i++) {
            const categoryCell = rows[i].getElementsByTagName('td')[0];
            if (categoryCell) {
                const categoryValue = categoryCell.textContent.trim().toLowerCase();
                if (categoryValue !== category.toLowerCase()) {
                    rows[i].style.display = 'none';
                } else {
                    rows[i].style.display = '';
                }
            }
        }
    }
    function clearFilter() {
        const rows = table.getElementsByTagName('tr');
        for (let i = 0; i < rows.length; i++) {
            rows[i].style.display = '';
        }
    }
});

