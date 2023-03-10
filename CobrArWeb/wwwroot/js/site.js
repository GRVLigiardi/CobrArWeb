function addToCart(codeBarre) {
    // Ajoute le produit au panier
    var cartItems = localStorage.getItem("cartItems");
    if (!cartItems) {
        cartItems = [];
    } else {
        cartItems = JSON.parse(cartItems);
    }
    cartItems.push(codeBarre);
    localStorage.setItem("cartItems", JSON.stringify(cartItems));

    // Met à jour l'affichage du panier
    var cartList = document.getElementById("cart-items");
    var product = document.createElement("li");
    product.innerText = codeBarre;
    cartList.appendChild(product);
}
