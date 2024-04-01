﻿namespace TeacherRateProject.Services.Interfaces;

public interface IRatingTaskService<T, TKey> 
{
    Task<T> Add(T task);
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(TKey id);
}
