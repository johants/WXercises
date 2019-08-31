using System;
using System.Collections.Generic;
using System.Linq;
using WXercises.Enums;
using WXercises.Models;

namespace WXercises.UnitTests.Mocks
{
    public class MockData
    {
        public static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Test Product A",
                    Price = 99.99m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product B",
                    Price = 101.99m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product C",
                    Price = 10.99m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product D",
                    Price = 5.0m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product F",
                    Price = 999999999999.0m,
                    Quantity = 0.0
                },
            };
            return products;
        }

        public static List<Product> GetProductSorted(SortOption sortOption)
        {
            var products = GetProducts();

            switch (sortOption)
            {
                case SortOption.Low:
                    return products.OrderBy(p => p.Price).ToList();
                case SortOption.High:
                    return products.OrderByDescending(p => p.Price).ToList();
                case SortOption.Ascending:
                    return products.OrderBy(p => p.Name).ToList();
                case SortOption.Descending:
                    return products.OrderByDescending(p => p.Name).ToList();
                case SortOption.Recommended:
                    return GetRecommendedProduct();
                default:
                    return products;
            }
        }

        public static List<Product> GetRecommendedProduct()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Test Product A",
                    Price = 99.99m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product B",
                    Price = 101.99m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product F",
                    Price = 999999999999.0m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product C",
                    Price = 10.99m,
                    Quantity = 0.0
                },
                new Product
                {
                    Name = "Test Product D",
                    Price = 5.0m,
                    Quantity = 0.0
                }
            };
            return products;
        }

        public static Decimal GetTrolleyTotal()
        {
            return 5.0m;
        }

        public static List<ShopperHistory> GetShopperHistory()
        {
            return new List<ShopperHistory>
            {
                new ShopperHistory()
                {
                    CustomerId = 123,
                    Products = new List<Product>
                    {
                        new Product()
                        {
                            Name = "Test Product A",
                            Price = 99.99m,
                            Quantity = 3.0
                        },
                        new Product
                        {
                            Name = "Test Product B",
                            Price = 101.99m,
                            Quantity = 1.0
                        },
                        new Product
                        {
                            Name = "Test Product F",
                            Price = 999999999999.0m,
                            Quantity = 1.0
                        },
                    }
                },
                new ShopperHistory()
                {
                    CustomerId = 23,
                    Products = new List<Product>
                    {
                        new Product()
                        {
                            Name = "Test Product A",
                            Price = 99.99m,
                            Quantity = 2.0
                        },
                        new Product
                        {
                            Name = "Test Product B",
                            Price = 101.99m,
                            Quantity = 3.0
                        },
                        new Product
                        {
                            Name = "Test Product F",
                            Price = 999999999999.0m,
                            Quantity = 1.0
                        },
                    }
                },
                new ShopperHistory()
                {
                    CustomerId = 23,
                    Products = new List<Product>
                    {
                        new Product()
                        {
                            Name = "Test Product C",
                            Price = 10.99m,
                            Quantity = 2.0
                        },
                        new Product
                        {
                            Name = "Test Product F",
                            Price = 999999999999.0m,
                            Quantity = 2.0
                        },
                    }
                },
                new ShopperHistory()
                {
                    CustomerId = 23,
                    Products = new List<Product>
                    {
                        new Product()
                        {
                            Name = "Test Product A",
                            Price = 99.99m,
                            Quantity = 1.0
                        },
                        new Product
                        {
                            Name = "Test Product B",
                            Price = 101.99m,
                            Quantity = 1.0
                        },
                        new Product
                        {
                            Name = "Test Product c",
                            Price = 10.99m,
                            Quantity = 1.0
                        },
                    }
                },
            };
        }
    }
}
