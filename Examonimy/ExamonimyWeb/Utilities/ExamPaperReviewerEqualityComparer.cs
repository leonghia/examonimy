using ExamonimyWeb.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ExamonimyWeb.Utilities
{
    public class ExamPaperReviewerEqualityComparer : IEqualityComparer<ExamPaperReviewer>
    {
        public bool Equals(ExamPaperReviewer? x, ExamPaperReviewer? y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (y is null || x is null)
                return false;
            return x.ExamPaperId == y.ExamPaperId && x.ReviewerId == y.ReviewerId;
        }

        public int GetHashCode([DisallowNull] ExamPaperReviewer obj)
        {
            return obj.ExamPaperId ^ obj.ReviewerId;
        }
    }
}
