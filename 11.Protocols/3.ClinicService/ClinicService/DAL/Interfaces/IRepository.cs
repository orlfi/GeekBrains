﻿namespace ClinicService.DAL.Interfaces
{
    public interface IRepository<T, TId>
    {
        TId? Add(T item);

        void Update(T item);

        void Delete(T item);

        void Delete(TId id);

        IList<T> GetAll();

        T GetById(TId id);
    }
}
