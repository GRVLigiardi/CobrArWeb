$(document).ready(function () {
    var expectedAdminAccessCode = "Futbol";

    // Intercepte le clic sur le lien "Créez un compte" et ouvrez la boîte de dialogue modale.
    $('a#createAccountLink').on('click', function (e) {
        e.preventDefault();
        $('#adminAccessCodeModal').modal('show');
    });

    // Vérifie le code d'accès administrateur et redirigez vers la vue "CreateUser" si le code est correct.
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
