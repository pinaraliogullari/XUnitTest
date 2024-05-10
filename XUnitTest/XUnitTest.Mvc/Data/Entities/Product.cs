using System.ComponentModel.DataAnnotations;

namespace XUnitTest.Mvc.Data.Entities
{
	public class Product
	{
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

		[Required]
		public string Color { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public int Stock { get; set; }
    }
}
