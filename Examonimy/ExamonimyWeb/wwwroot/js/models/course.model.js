export class Course {
    id = 0;
    name = "";
    courseCode = "";
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