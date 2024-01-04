export class Exam {
    id = 0;
    mainClasses = [""];
    examPaperCode = "";
    courseName = "";
    from = "";
    to = "";
    timeAllowedInMinutes = 0;
}

export class ExamCreate {
    mainClassIds = [0];
    examPaperId = 0;
    from = new Date();
    to = new Date();
}