/**
 * Performs mathematical operations on two numbers
 * @param {number} a - first number
 * @param {number} b - second number
 * @returns {number} - the result of summing the numbers
 */
function sum(a, b) {
    return a+b;
}

/**
 * Performs mathematical operations on two numbers
 * @param {*} arg1 - first number
 * @param {*} arg2 - second number
 * @param {*} operation - mathematical operation
 * @returns - the result of performing mathematical operations on two numbers
 */
function mathOperation(arg1, arg2, operation) {
    switch (operation) {
      case "+":
        return arg1 + arg2;
      case "-":
        return arg1 - arg2;
      case "*":
        return arg1 * arg2;
      case "/":
        if (arg2 === 0) {
          alert(`Деление на 0: arg2 = ${arg2})`);
          return NaN;
        }
        return arg1 / arg2;
      default:
        alert(`Недопустимая операция: operation = ${operation}`);
        return NaN;
    }
  }