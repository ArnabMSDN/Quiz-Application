﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz_Application.Services.Entities;

namespace Quiz_Application.Services.Repository.Interfaces
{
    public interface IResult<TEntity>
    {
        Task<IEnumerable<Attempt>> GetAttemptHistory(string argCandidateID);
        Task<IEnumerable<TEntity>> ScoreReport(ReqReport argRpt);
        Task<int> AddResult(List<TEntity> entity);
    }
}
