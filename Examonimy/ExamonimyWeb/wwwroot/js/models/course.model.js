export class Course {
    id = 0;
    name = "";
    courseCode = "";
    numbersOfExamPapers = 0;

    constructor(id = 0, name = "", courseCode = "", numbersOfExamPapers = 0) {
        this.id = id;
        this.name = name;
        this.courseCode = courseCode;
        this.numbersOfExamPapers = numbersOfExamPapers;
    }
}