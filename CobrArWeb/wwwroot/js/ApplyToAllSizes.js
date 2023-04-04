document.getElementById("applyToAllSizes").addEventListener("change", function () {
    var warningMessage = document.getElementById("warning-message");
    if (this.checked) {
        warningMessage.style.display = "block";
    } else {
        warningMessage.style.display = "none";
    }
});