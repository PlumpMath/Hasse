using System;
using System.Collections.Generic;
using Hasse.Groups;

namespace Hasse {
    public class Generator<TSub> where TSub : ISubGroup<TSub>, IEquatable<TSub> {
        private IGroup<TSub> Group {
            get;
            set;
        }

        public Generator(IGroup<TSub> group) {
            Group = group;
        }

        public IEnumerable<TSub> Generate() {
            var generated = new List<TSub>();
            for(uint i = 0; i < Group.Order; i++) {
                TSub single = Group.Generate(i);
                if(!generated.Contains(single)) {
                    generated.Add(single);
                    Generate(generated, single);
                }
            }
            return generated;
        }

        private void Generate(List<TSub> generated, TSub curr) {
            for(uint i = 0; i < Group.Order; i++) {
                if(!curr.Contains(i)) {
                    TSub next = curr.Generate(i);
                    if(!generated.Contains(next)) {
                        generated.Add(next);
                        Generate(generated, next);
                    }
                }
            }
        }
    }
}
