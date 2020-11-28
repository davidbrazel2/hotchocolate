﻿using HotChocolate.Data.Neo4J.Language;

namespace HotChocolate.Data.Neo4J
{
    public partial class CypherVisitor
    {
        public void Enter(Visitable visitable)
        {
            switch (visitable.Kind)
            {
                case ClauseKind.Match:
                    EnterVisitable((Match)visitable);
                    break;
                case ClauseKind.Where:
                    EnterVisitable((Where)visitable);
                    break;
                case ClauseKind.Create:
                    EnterVisitable((Create)visitable);
                    break;
                case 0:
                    break;
            }
        }
    }
}