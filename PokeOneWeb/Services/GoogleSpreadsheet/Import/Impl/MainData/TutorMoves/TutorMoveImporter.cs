using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.TutorMoves
{
    public class TutorMoveImporter : SpreadsheetEntityImporter<TutorMoveDto, TutorMove>
    {
        private readonly ApplicationDbContext _dbContext;

        public TutorMoveImporter(
            ISpreadsheetEntityReader<TutorMoveDto> reader,
            ISpreadsheetEntityMapper<TutorMoveDto, TutorMove> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_TUTOR_MOVES;
        }

        protected override void WriteToDatabase(IEnumerable<TutorMove> newTutorMoves)
        {
            _dbContext.TutorMoves.AddRange(newTutorMoves);
            _dbContext.SaveChanges();
        }
    }
}
