/**
 * Объект игрока, здесь будут все методы и свойства связанные с ним.
 * @property {int} x Позиция по X-координате.
 * @property {int} y Позиция по Y-координате.
 */
const player = {
  x: 0,
  y: 0,

  /**
   * Двигает игрока по переданному направлению.
   * @param {{x: int, y: int}} nextPoint Следующая точка пользователя.
   */
  move(nextPoint) {
    if (
        nextPoint.x >= 0 &&
        nextPoint.x <= config.colsCount &&
        nextPoint.y >= 0 &&
        nextPoint.y <= config.rowsCount
    ) {
      this.x = nextPoint.x;
      this.y = nextPoint.y;
    }
  },

  /**
   * Checks whether the point does not go beyond the boundaries
   * @param {{x: int, y: int}} point Point coordinates
   * @returns {boolean} Verification result
   */
  isPointInBounds(point){
    return nextPoint.x >= 0 &&
      nextPoint.x <= config.colsCount &&
      nextPoint.y >= 0 &&
      nextPoint.y <= config.rowsCount;
  }
};
