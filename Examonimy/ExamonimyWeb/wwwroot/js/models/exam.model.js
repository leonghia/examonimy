export class Exam {
    id = 0;
    mainClassName = "";
    examPaperCode = "";
    courseName = "";
    from = "";
    to = "";
    timeAllowedInMinutes = 0;
}

export class ExamCreate {
    mainClassId;
    examPaperId;
    from;
    to;
}