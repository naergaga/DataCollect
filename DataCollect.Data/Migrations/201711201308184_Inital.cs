namespace DataCollect.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sheet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Column",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SheetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sheet", t => t.SheetId, cascadeDelete: true)
                .Index(t => t.SheetId);
            
            CreateTable(
                "dbo.ColumnData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false),
                        ColumnId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Column", t => t.ColumnId, cascadeDelete: true)
                .Index(t => t.ColumnId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ColumnData", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.Column", "SheetId", "dbo.Sheet");
            DropForeignKey("dbo.Sheet", "BookId", "dbo.Book");
            DropIndex("dbo.ColumnData", new[] { "ColumnId" });
            DropIndex("dbo.Column", new[] { "SheetId" });
            DropIndex("dbo.Sheet", new[] { "BookId" });
            DropTable("dbo.ColumnData");
            DropTable("dbo.Column");
            DropTable("dbo.Sheet");
            DropTable("dbo.Book");
        }
    }
}
