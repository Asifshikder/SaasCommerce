﻿using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Project.Core.Domain.Contracts;
using Project.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Project.Infrastructure.Persistence;

namespace Project.Persistence
{
    public sealed class EntityRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
      where T : class, IAggregateRoot
    {
        public EntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        // We override the default behavior when mapping to a dto.
        // We're using Mapster's ProjectToType here to immediately map the result from the database.
        // This is only done when no Selector is defined, so regular specifications with a selector also still work.
        protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
            specification.Selector is not null
                ? base.ApplySpecification(specification)
                : ApplySpecification(specification, false)
                    .ProjectToType<TResult>();
    }

}
