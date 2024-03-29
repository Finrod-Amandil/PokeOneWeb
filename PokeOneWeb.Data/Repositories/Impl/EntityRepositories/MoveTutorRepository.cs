﻿using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class MoveTutorRepository : HashedEntityRepository<MoveTutor>
    {
        public MoveTutorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override List<Func<MoveTutor, bool>> PreparationSteps => new()
        {
            entity => TrySetIdForName<Location>(entity.LocationName, id => entity.LocationId = id)
        };
    }
}