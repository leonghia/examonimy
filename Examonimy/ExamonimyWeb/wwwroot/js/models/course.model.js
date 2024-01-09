export class Course {
    id;
    name;
    courseCode;

    constructor(id = 0, name = "", courseCode = "") {
        this.id = id;
        this.name = name;
        this.courseCode = courseCode;
    }
}

export class CourseWithNumbersOfExamPapers extends Course {
    numbersOfExamPapers = 0;

    constructor() {
        super();      
    }
}

export class CourseWithNumbersOfQuestions extends Course {  
    numbersOfQuestions = 0;
    constructor() {  
        super();        
    }
}