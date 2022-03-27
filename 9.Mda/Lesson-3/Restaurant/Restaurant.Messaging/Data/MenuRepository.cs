﻿namespace Restaurant.Messaging.Data;

public static class MenuRepository
{
    private static List<Dish> _menu = new()
    {
        new Dish(1, "Рыба"),
        new Dish(2, "Мясо"),
        new Dish(3, "Суп"),
    };

    public static int Count => _menu.Count;

    public static Dish? GetDishById(int id)
    {
        var result = _menu.SingleOrDefault(item => item.Id == id);
        return result;
    }
}
