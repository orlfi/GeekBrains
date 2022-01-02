using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailService.Domain.Entities;
using MailService.Interfaces.Common;

namespace MailService.DAL;

public class MemoryDatabase
{
    private Dictionary<Type, object> _sets = new Dictionary<Type, object>();
    public IList<Employee> Employees { get; set; } = new List<Employee>();
    public IList<Product> Products { get; set; } = new List<Product>();

    public MemoryDatabase()
    {
        _sets.Add(typeof(Employee), Employees);
        _sets.Add(typeof(Product), Products);
        InitializeWithData();
    }

    public IList<T> Set<T>() where T : IEntity
    {
        if (!_sets.TryGetValue(typeof(T), out var set))
            throw new NullReferenceException($"В базе отсутствует набор сущностей {typeof(T)}");

        return (IList<T>)set;
    }

    public void InitializeWithData()
    {
        Products.Add(new Product()
        {
            Id = 1,
            Name = "Бананы",
            Price = 65
        });

        Products.Add(new Product()
        {
            Id = 2,
            Name = "Апельсины",
            Price = 70
        });

        Products.Add(new Product()
        {
            Id = 3,
            Name = "Виноград",
            Price = 120
        });

        Products.Add(new Product()
        {
            Id = 4,
            Name = "Яблоки",
            Price = 78
        });

        Employees.Add(new Employee()
        {
            Id = 1,
            Name = "Иванов Иван Иванович",
            Email = "orlfi@mai.ru"
        });

        Employees.Add(new Employee()
        {
            Id = 1,
            Name = "Петров Петр Петрович",
            Email = "orlfi@yandex.ru"
        });

        for (int i = 0; i < Employees.Count; i++)
        {
            var employee = Employees[i];
            for (int j = 0; j < Random.Shared.Next(3, 5); j++)
            {
                employee.Orders.Add(new Order()
                {
                    Id = j,
                    Product = Products[Random.Shared.Next(0, Products.Count)],
                    Count = Random.Shared.Next(1, 10)
                });
            }
        }
    }
}
