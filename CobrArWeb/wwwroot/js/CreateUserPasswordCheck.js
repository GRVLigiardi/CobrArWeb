$(document).ready(function () {
    var expectedAdminAccessCode = "Futbol";

    // Intercepte le clic sur le lien "Cr�ez un compte" et ouvrez la bo�te de dialogue modale.
    $('a#createAccountLink').on('click', function (e) {
        e.preventDefault();
        $('#adminAccessCodeModal').modal('show');
    });

    // V�rifie le code d'acc�s administrateur et redirigez vers la vue "CreateUser" si le code est correct.
    $('#checkAccessCode').on('click', function () {
        var adminAccessCode = $('#AdminAccessCode').val();
        if (adminAccessCode === expectedAdminAccessCode) {
            $('#adminAccessCodeModal').modal('hide');
            window.location.href = '/Home/CreateUser';
        } else {
            alert('Code incorrect');
        }
    });
});
