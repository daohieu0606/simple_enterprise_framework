namespace UI.Views
{
    using Core.Database;
    using System.Data;
    using UI.Model;

    public interface RootForm
    {
        IDatabase database { get; set; }

        DataTable data { get; set; }

        StyleOption styleOption { get; set; }

        string tableName { get; set; }

        void InitStyle();
    }
}
