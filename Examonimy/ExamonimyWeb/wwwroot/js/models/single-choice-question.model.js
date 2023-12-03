import { Question } from "./question.model";

export class SingleChoiceQuestion extends Question {
    choiceA = "";
    choiceB = "";
    choiceC = "";
    choiceD = "";

    correctAnswer = 0;
}