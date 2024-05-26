namespace ItemsAPI
{
    using ItemsAPI.Models;

    public class ItemValidator
    {
        public bool IsValidItem(Item item, out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (item.Price <= 0)
            {
                validationErrors.Add("Price must be a positive number");
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                validationErrors.Add("Name can't be empty");
            }

            if (item.Name.Length > 100)
            {
                validationErrors.Add("Name can't be longer than 100 chars");
            }

            return !validationErrors.Any();
        }
    }
}
