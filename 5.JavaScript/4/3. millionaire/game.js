/**
 *
 */
const game = {
  round: 0,
  /**
   * Запускает игру.
   */
  run() {
    // Бесконечный цикл
    while (true) {
      if (this.round > questions.length - 1) {
        this.showScore();
        if (!this.continueGame()) {
          return;
        }
      }

      this.showQuestion();

      const answer = this.getAnswer();
      if (answer === null) {
        console.log("Игра окончена.");
        return;
      }
      this.checkAnswer(answer);

      this.round++;
    }
  },

  /**
   * Проверяет правильность ответа игрока
   * @param {string} answer ответ игрока
   */
  checkAnswer(answer) {
    if (questions[this.round].correct === answer) {
      player.score++;
    }
  },

  /**
   * запрашивает продолжение игры
   * @returns {boolean} true-продолжить игру, false - завершить игру
   */
  continueGame() {
    if (confirm("Хотите сыграть еще раз?")) {
      this.round = 0;
      player.score = 0;
      return true;
    } else {
      return false;
    }
  },

  /**
   * Выводит вопрос со списком ответов и запрашивает правильный ответ у игрока
   * @returns {string} возвращает код ответа, null для выхода из игры
   */
  getAnswer() {
    const answer = prompt(
      "Введите вариант ответа или любой другой символ для выхода"
    )[0];
    if (answer === "a" || answer === "b" || answer === "c" || answer === "d") {
      return answer;
    } else {
      return null;
    }
  },

  /**
   * Выводит вопрос со списком ответов
   */
  showQuestion() {
    let text = "";
    const question = questions[this.round];

    console.clear();
    text += `Раунд ${this.round + 1}\n`;
    text += `${question.question}\n\n`;

    for (let answerKey in question.answers) {
      text += `${answerKey}. ${question.answers[answerKey]}\n`;
    }

    console.log(text);
  },

  /**
   * Выводит счет игрока
   */
  showScore() {
    let text = "";
    console.clear();
    text += "Игра окончена.\n";
    text += `Ваш счет ${player.score}\n`;
    console.log(text);
  },

  // Этот метод выполняется при открытии страницы.
  init() {
    console.log("Кто хочет стать миллионером?");
    console.log("Чтобы начать игру наберите game.run() и нажмите Enter.");
  },
};

game.init();
