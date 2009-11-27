using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Orchard.Models.Records;

namespace Orchard.Models {

    public interface IContentQuery {
        IContentManager ContentManager { get; }
        IContentQuery<TPart> ForPart<TPart>() where TPart : IContent;
    }

    public interface IContentQuery<TPart> : IContentQuery where TPart : IContent {
        IContentQuery<TPart> ForType(params string[] contentTypes);
        IEnumerable<TPart> List();
        IEnumerable<TPart> Slice(int skip, int count);

        IContentQuery<TPart, TRecord> Join<TRecord>() where TRecord : ContentPartRecord;

        IContentQuery<TPart, TRecord> Where<TRecord>(Expression<Func<TRecord, bool>> predicate) where TRecord : ContentPartRecord;
        IContentQuery<TPart, TRecord> OrderBy<TRecord, TKey>(Expression<Func<TRecord, TKey>> keySelector) where TRecord : ContentPartRecord;
        IContentQuery<TPart, TRecord> OrderByDescending<TRecord, TKey>(Expression<Func<TRecord, TKey>> keySelector) where TRecord : ContentPartRecord;
    }

    public interface IContentQuery<TPart, TRecord> : IContentQuery<TPart> where TPart : IContent where TRecord : ContentPartRecord {
        IContentQuery<TPart, TRecord> Where(Expression<Func<TRecord, bool>> predicate);
        IContentQuery<TPart, TRecord> OrderBy<TKey>(Expression<Func<TRecord, TKey>> keySelector);
        IContentQuery<TPart, TRecord> OrderByDescending<TKey>(Expression<Func<TRecord, TKey>> keySelector);
    }

}