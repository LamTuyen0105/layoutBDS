using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeOfTransaction>().HasData(
                new TypeOfTransaction() { Id = 1, TypeOfTransactionName = "Cần Bán" },
                new TypeOfTransaction() { Id = 2, TypeOfTransactionName = "Cho Thuê" },
                new TypeOfTransaction() { Id = 3, TypeOfTransactionName = "Cần Mua" },
                new TypeOfTransaction() { Id = 4, TypeOfTransactionName = "Cần Thuê" }
                );
            modelBuilder.Entity<Direction>().HasData(
                new Direction() { Id = 1, DirectionName = "Đông" },
                new Direction() { Id = 2, DirectionName = "Tây" },
                new Direction() { Id = 3, DirectionName = "Nam" },
                new Direction() { Id = 4, DirectionName = "Bắc" },
                new Direction() { Id = 5, DirectionName = "Đông Bắc" },
                new Direction() { Id = 6, DirectionName = "Tây Bắc" },
                new Direction() { Id = 7, DirectionName = "Đông Nam" },
                new Direction() { Id = 8, DirectionName = "Tây Nam" }
                );
            modelBuilder.Entity<EvaluationStatus>().HasData(
                new EvaluationStatus() { Id = 1, EvaluationStatusName = "Đã Thẩm Định" },
                new EvaluationStatus() { Id = 2, EvaluationStatusName = "Chưa Thẩm Định" }
                );
            modelBuilder.Entity<LegalPaper>().HasData(
                new LegalPaper() { Id = 1, TypeOfLegalPapers = "Sổ Hồng" },
                new LegalPaper() { Id = 2, TypeOfLegalPapers = "Sổ Đỏ" },
                new LegalPaper() { Id = 3, TypeOfLegalPapers = "Giấy Tay" },
                new LegalPaper() { Id = 4, TypeOfLegalPapers = "Giấy Tờ Hợp Lệ" },
                new LegalPaper() { Id = 5, TypeOfLegalPapers = "Đang Hợp Thức Hóa" },
                new LegalPaper() { Id = 6, TypeOfLegalPapers = "Chủ Quyền Tư Nhân" },
                new LegalPaper() { Id = 7, TypeOfLegalPapers = "Hợp Đồng" }
                );
            modelBuilder.Entity<TypeOfProperty>().HasData(
                new TypeOfProperty() { Id = 1, TypeOfPropertyName = "Chung Cư" },
                new TypeOfProperty() { Id = 2, TypeOfPropertyName = "Căn Hộ" },
                new TypeOfProperty() { Id = 3, TypeOfPropertyName = "Nhà Riêng" },
                new TypeOfProperty() { Id = 4, TypeOfPropertyName = "Đất Nền" }
                );
            
        }
    }
}
