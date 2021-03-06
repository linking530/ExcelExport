using SimpleJSON;
using System.Collections.Generic;

namespace Config
{
	public class example
	{
		public int key1 {get;private set;}
		public int someid {get;private set;}
		public float rate {get;private set;}
		public string name {get;private set;}
		public List<int> testarray {get;private set;}
		public List<KeyValuePair<int, int>> testpairarray {get;private set;}
		public string testcustomtype {get;private set;}

		static SortedDictionary<string,example> _datas = new SortedDictionary<string,example>();

		static public example Find(int key1, float rate)
		{
			var _key =  key1.ToString() +  rate.ToString();
			if(_datas.ContainsKey(_key))
			{
				return _datas[_key];
			}
			return null;
		}

		static bool Add( int key1, float rate,example v)
		{
			var _key =  key1.ToString() +  rate.ToString();
			if(!_datas.ContainsKey(_key))
			{
				_datas.Add(_key,v);
				return true;
			}
			return false;
		}

		static public bool Load(string content)
		{
			_datas.Clear();
			var parser = JSONNode.Parse(content);
			var dataArray = parser.AsArray;
			foreach (var data in dataArray.Childs)
			{
				example d = new example();
				d.key1 = data["key1"].AsInt;
				d.someid = data["someid"].AsInt;
				d.rate = data["rate"].AsFloat;
				d.name = data["name"].Value;
				d.testarray = StringUtil.ParseArray<int>(data["testarray"].Value,'|');
				d.testpairarray = StringUtil.ParsePairArray<int,int>(data["testpairarray"].Value,'|',',');
				d.testcustomtype = data["testcustomtype"].Value;
				if(!Add(d.key1,d.rate,d))
				{
					return false;
				}
			}
			return true;
		}
	}
}
