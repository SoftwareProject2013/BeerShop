namespace BeerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        stockCount = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        isStillOnSale = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.CategoryItem",
                c => new
                    {
                        CategoryItemID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        Item_ItemID = c.Int(),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryItemID)
                .ForeignKey("dbo.Item", t => t.Item_ItemID)
                .ForeignKey("dbo.Category", t => t.Category_CategoryID)
                .Index(t => t.Item_ItemID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        content = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                        author_UserID = c.Int(nullable: false),
                        item_ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.User", t => t.author_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.item_ItemID, cascadeDelete: true)
                .Index(t => t.author_UserID)
                .Index(t => t.item_ItemID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        phone = c.String(),
                        address = c.String(),
                        birth = c.DateTime(nullable: false),
                        locked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comment", new[] { "item_ItemID" });
            DropIndex("dbo.Comment", new[] { "author_UserID" });
            DropIndex("dbo.CategoryItem", new[] { "Category_CategoryID" });
            DropIndex("dbo.CategoryItem", new[] { "Item_ItemID" });
            DropForeignKey("dbo.Comment", "item_ItemID", "dbo.Item");
            DropForeignKey("dbo.Comment", "author_UserID", "dbo.User");
            DropForeignKey("dbo.CategoryItem", "Category_CategoryID", "dbo.Category");
            DropForeignKey("dbo.CategoryItem", "Item_ItemID", "dbo.Item");
            DropTable("dbo.Category");
            DropTable("dbo.User");
            DropTable("dbo.Comment");
            DropTable("dbo.CategoryItem");
            DropTable("dbo.Item");
        }
    }
}
