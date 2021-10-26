const cart = new Cart();
const basketEl = document.querySelector(".basket");
const cartIconWrapEl = document.querySelector(".cartIconWrap");
const cartIconEl = cartIconWrapEl.querySelector(" span");
const basketTotalEl = basketEl.querySelector(".basketTotal");
const basketTotalValueEl = basketTotalEl.querySelector(".basketTotalValue");

/**
 * Hides or displays the basket
 */
cartIconWrapEl.addEventListener("click", (event) => {
    basketEl.classList.toggle("hidden");
});

/**
 * Event handler of clicking on the button to add an item to the cart
 */
document.querySelector(".featuredItems").addEventListener("click", (event) => {
    if (event.target.tagName == "BUTTON") {
        const featuredDataEl = event.target
            .closest(".featuredItem")
            .querySelector(".featuredData");

        addProductToCart(
            new Product(
                featuredDataEl.dataset.id,
                featuredDataEl.dataset.name,
                featuredDataEl.dataset.price
            )
        );
    }
});

/**
 * Event handler for clicking on the delete cart item button
 */
document.querySelector(".basket").addEventListener("click", (event) => {
    if (event.target.tagName == "IMG") {
        const basketRowEl = event.target.closest(".basketRow");
        deleteProductFromCart(basketRowEl.dataset.id);
    }
});

/**
 * Adds an product to the cart
 * @param {object} product product object
 */
function addProductToCart(product) {
    cart.addProduct(product);
    updateCartIcon(cart.getCount());
    renderRows();
    updateBasketTotal(cart.getTotal());
}

/**
 * Removes an item from the shopping cart
 * @param {number} id product ID
 */
function deleteProductFromCart(id) {
    cart.deleteProductById(id);
    updateCartIcon(cart.getCount());
    deleteRowById(id);
    updateBasketTotal(cart.getTotal());
}

/**
 * Updates the icon of the number of products
 * @param {number} value number of products
 */
function updateCartIcon(value) {
    cartIconEl.textContent = cart.getCount();
}

/**
 * Renders cart rows
 */
function renderRows() {
    const cardItems = cart.getItems();
    cardItems.forEach((cartItem) => renderRow(cartItem));
}

/**
 * Removes a row from the markup
 * @param {number} id product ID
 */
function deleteRowById(id) {
    const rowEl = basketEl.querySelector(`[data-id="${id}"]`);
    rowEl.remove();
}

/**
 * Renders cart row
 * @param {object} cartItem cart item
 */
function renderRow(cartItem) {
    const rowHtml = getRowHtml(cartItem);
    const rowEl = basketEl.querySelector(`[data-id="${cartItem.product.id}"]`);
    if (rowEl) {
        rowEl.outerHTML = rowHtml;
    } else {
        basketTotalEl.insertAdjacentHTML("beforebegin", rowHtml);
    }
}

/**
 * returns the HTML markup of the cart item
 * @param {object} cartItem cart item
 * @returns {string} HTML markup
 */
function getRowHtml(cartItem) {
    return `<div class="basketRow" data-id="${cartItem.product.id}">
    <div>${cartItem.product.name}</div>
    <div>${cartItem.count}</div>
    <div>${cartItem.product.price}</div>
    <div>${cartItem.getSum()}</div>
    <img class="deleteItem" src="images/delete.svg" alt="">
</div>`;
}

/**
 * Updates the total amount of the basket
 * @param {*number} value total value
 */
function updateBasketTotal(value) {
    basketTotalValueEl.textContent = value;
}
