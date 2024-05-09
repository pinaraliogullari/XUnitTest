﻿namespace XUnitTest.Mvc.Data.Entities
{
	public class Product
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
