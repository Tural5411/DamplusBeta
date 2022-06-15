using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Heç bir kateqoriya tapılmadı";
                return "Bu cür kateqoriya tapılmadı";
            }
            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kateqoriya uğurla əlavə edildi.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kateqoriya uğurla silindi.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kateqoriya uğurla databazadan silindi.";
            }

            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} adlı kateqoriya arxivdən geri gətirildi.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kateqoriya uğurla yeniləndi";
            }
        }
        public static class Video
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Heç bir video tapılmadı";
                return "Bu cür video tapılmadı";
            }
            public static string Add(string videoName)
            {
                return $"{videoName} adlı video uğurla əlavə edildi.";
            }
            public static string Delete(string videoName)
            {
                return $"{videoName} adlı video uğurla silindi.";
            }
            public static string HardDelete(string videoName)
            {
                return $"{videoName} adlı video uğurla databazadan silindi.";
            }

            public static string UndoDelete(string videoName)
            {
                return $"{videoName} adlı video arxivdən geri gətirildi.";
            }
            public static string Update(string videoName)
            {
                return $"{videoName} adlı video uğurla yeniləndi";
            }
        }
        public static class Team
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Heç bir team tapılmadı";
                return "Bu cür team tapılmadı";
            }
            public static string Add(string teamName)
            {
                return $"{teamName} adlı team uğurla əlavə edildi.";
            }
            public static string Delete(string teamName)
            {
                return $"{teamName} adlı team uğurla silindi.";
            }
            public static string HardDelete(string teamName)
            {
                return $"{teamName} adlı team uğurla databazadan silindi.";
            }

            public static string UndoDelete(string teamName)
            {
                return $"{teamName} adlı team arxivdən geri gətirildi.";
            }
            public static string Update(string teamName)
            {
                return $"{teamName} adlı team uğurla yeniləndi";
            }
        }
        public static class Project
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Heç bir proyekt tapılmadı";
                return "Bu cür proyekt tapılmadı";
            }
            public static string Add(string projectName)
            {
                return $"{projectName} adlı proyekt uğurla əlavə edildi.";
            }
            public static string Delete(string projectName)
            {
                return $"{projectName} adlı proyekt uğurla silindi.";
            }
            public static string HardDelete(string projectName)
            {
                return $"{projectName} adlı proyekt uğurla databazadan silindi.";
            }

            public static string UndoDelete(string projectName)
            {
                return $"{projectName} adlı proyekt arxivdən geri gətirildi.";
            }
            public static string Update(string projectName)
            {
                return $"{projectName} adlı proyekt uğurla yeniləndi";
            }
        }
        public static class ProjectCategory
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Heç bir proyekt kateqoriyasi tapılmadı";
                return "Bu cür proyekt tapılmadı";
            }
            public static string Add(string projectName)
            {
                return $"{projectName} adlı proyekt kateqoriyasi uğurla əlavə edildi.";
            }
            public static string Delete(string projectName)
            {
                return $"{projectName} adlı proyekt kateqoriyasi uğurla silindi.";
            }
            public static string HardDelete(string projectName)
            {
                return $"{projectName} adlı proyekt kateqoriyasi uğurla databazadan silindi.";
            }

            public static string UndoDelete(string projectName)
            {
                return $"{projectName} adlı proyekt kateqoriyasi arxivdən geri gətirildi.";
            }
            public static string Update(string projectName)
            {
                return $"{projectName} adlı proyekt kateqoriyasi uğurla yeniləndi";
            }
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {

                if (isPlural) return "Heç bir məqalə tapılmadı";
                return "Bu cür məqalə tapılmadı";
            }

            public static string Add(string articleName)
            {
                return $"{articleName} adlı məqalə uğurla əlavə edildi.";
            }
            public static string Delete(string articleName)
            {
                return $"{articleName} adlı məqalə uğurla silindi.";
            }
            public static string HardDelete(string articleName)
            {
                return $"{articleName} adlı məqalə uğurla databazadan silindi.";
            }
            public static string UndoDelete(string articleName)
            {
                return $"{articleName} adlı məqalə arxivdən geri gətirildi.";
            }
            public static string Update(string articleName)
            {
                return $"{articleName} adlı məqalə uğurla yeniləndi";
            }
            public static string IncreaseViewCount(string articleName)
            {
                return $"{articleName} adlı məqalə oxu sayı artırıldı.";
            }
        }
        public static class Business
        {
            public static string NotFound(bool isPlural)
            {

                if (isPlural) return "Heç bir biznes tapılmadı";
                return "Bu cür biznes tapılmadı";
            }

            public static string Add(string businessName)
            {
                return $"{businessName} adlı biznes uğurla əlavə edildi.";
            }
            public static string Delete(string businessName)
            {
                return $"{businessName} adlı biznes uğurla silindi.";
            }
            public static string HardDelete(string businessName)
            {
                return $"{businessName} adlı biznes uğurla databazadan silindi.";
            }
            public static string UndoDelete(string businessName)
            {
                return $"{businessName} adlı biznes arxivdən geri gətirildi.";
            }
            public static string Update(string businessName)
            {
                return $"{businessName} adlı biznes uğurla yeniləndi";
            }
        }
        public static class Comment
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Heç bir şərh tapılmadı";
                return "Bu cür şərh tapılmadı";
            }
            public static string Add(string createdByName)
            {
                return $"{createdByName} adlı şərh uğurla əlavə edildi.";
            }
            public static string Approve(int commentId)
            {
                return $"{commentId} nömrəli şərh uğurla təsdiq edildi.";
            }
            public static string Delete(string createdByName)
            {
                return $"{createdByName} adlı şərh uğurla silindi.";
            }
            public static string HardDelete(string createdByName)
            {
                return $"{createdByName} adlı şərh uğurla databazadan silindi.";
            }
            public static string UndoDelete(string createdByName)
            {
                return $"{createdByName} istifadəçinə aid şərh arxivdən geri gətirildi.";
            }
            public static string Update(string createdByName)
            {
                return $"{createdByName} adlı şərh uğurla yeniləndi";
            }
        }
    }
}
