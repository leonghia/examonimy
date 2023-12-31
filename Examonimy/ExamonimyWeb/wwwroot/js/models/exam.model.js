﻿export class Exam {
    id = 0;
    mainClasses = [""];
    examPaperCode = "";
    courseName = "";
    from = "";
    to = "";
    timeAllowedInMinutes = 0;
}

export class ExamForStudent {
    id = 0;
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

export class ExamUpdate {
    examPaperId = 0;
    from = new Date();
    to = new Date();
}