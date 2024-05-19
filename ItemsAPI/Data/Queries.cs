namespace ItemsBlazorApp.Data
{
    public static class Queries
    {
        public const string InsertItemQuery =
            """
                INSERT INTO [Item] ([Name], [Price], [DateAdded])
                VALUES (@Name, @Price, @DateAdded)
            """;

        public const string GetItemQuery = 
            """
                SELECT [Id], [Name], [Price], [DateAdded] 
                FROM [Item] WHERE [Id] = @Id
            """;

        public const string UpdateItemQuery = 
            """
                UPDATE [Item] 
                SET [Name] = @Name, [Price] = @Price 
                WHERE [Id] = @Id
            """;

        public const string DeleteItemQuery = 
            """
                DELETE FROM [Item]
                WHERE [Id] = @Id
            """;

        public const string GetAllItemsQuery = 
            """
                SELECT * FROM [Item]
            """;

    }
}
