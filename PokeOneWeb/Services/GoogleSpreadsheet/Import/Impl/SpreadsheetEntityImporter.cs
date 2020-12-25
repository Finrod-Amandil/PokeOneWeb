using System;
using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public abstract class SpreadsheetEntityImporter<S, T> : ISpreadsheetEntityImporter<S, T> where S : ISpreadsheetEntityDto where T : class
    {
        private readonly ISpreadsheetEntityReader<S> _reader;
        private readonly ISpreadsheetEntityMapper<S, T> _mapper;

        protected SpreadsheetEntityImporter(ISpreadsheetEntityReader<S> reader, ISpreadsheetEntityMapper<S, T> mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        public void ImportFromSpreadsheet(Spreadsheet spreadsheet)
        {
            var dtos = _reader.Read(spreadsheet, GetSheetPrefix());
            var mappedEntities = _mapper.Map(dtos);
            WriteToDatabase(mappedEntities);
            dtos = null;
            mappedEntities = null;
            GC.Collect();
        }

        protected abstract string GetSheetPrefix();

        protected abstract void WriteToDatabase(IEnumerable<T> entities);
    }
}
