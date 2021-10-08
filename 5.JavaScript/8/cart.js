/**
 * Cart class
 */
class Cart {
    items = {};

    /**
     * Adds a product to cart
     * @param {object} product product object
     */
    addProduct(product) {
        if (product.id in this.items) {
            this.items[product.id].count++;
        } else {
            this.items[product.id] = new CartItem(product, 1);
        }
    }

    /**
     * Removes an product from the shopping cart
     * @param {number} id product ID
     */
    deleteProductById(id) {
        delete this.items[id];
    }

    /**
     * Returns an array of cart items
     * @returns {Array} cart items array
     */
    getItems() {
        return Object.values(this.items);
    }

    /**
     * Returns the number of cart items
     * @returns {number}
     */
    getCount() {
        let result = 0;
        for (const itemId in this.items) {
            const item = this.items[itemId];
            result += item.count;
        }
        return result;
    }

    /**
     * Returns the total cost of the items in the cart
     * @returns {number} total cost
     */
    getTotal() {
        let result = 0;
        for (const itemId in this.items) {
            const item = this.items[itemId];
            result += item.count * item.product.price;
        }
        return result;
    }
}
