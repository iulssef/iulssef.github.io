using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VectorEditor.History
{
    [Serializable]
    public class StackMemory
    {
        private readonly int _depth;
        private readonly List<byte[]> _list = new List<byte[]>();
        private int _index = -1;

        public StackMemory(int depth)
        {
            _depth = depth > 0 ? depth : 10;
        }

        public bool CanUndo => _index > 0;
        public bool CanRedo => _index < _list.Count - 1;

        public void Push(object obj)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                Push(ms);
            }
        }

        public void Push(MemoryStream stream)
        {
            if (_index < _list.Count - 1)
                _list.RemoveRange(_index + 1, _list.Count - _index - 1);

            _list.Add(stream.ToArray());
            _index = _list.Count - 1;

            while (_list.Count > _depth)
            {
                _list.RemoveAt(0);
                _index--;
            }
        }

        public MemoryStream Undo()
        {
            if (!CanUndo) return null;
            _index--;
            return GetCurrent();
        }

        public MemoryStream Redo()
        {
            if (!CanRedo) return null;
            _index++;
            return GetCurrent();
        }

        private MemoryStream GetCurrent()
        {
            var ms = new MemoryStream();
            ms.Write(_list[_index], 0, _list[_index].Length);
            ms.Position = 0;
            return ms;
        }

        public void Clear()
        {
            _list.Clear();
            _index = -1;
        }
    }
}