/**
 * Cart item class
 */
class CartItem {
    constructor(product, count)
    {
        this.product = product;
        this.count = count;
    }
    
    /**
     * Returns the amount of the product
     * @returns {number} amount
     */
    getSum() {
        return this.product.price * this.count;
    }
}
