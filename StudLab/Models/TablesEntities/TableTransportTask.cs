using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model
{
    public class TableTransportTask : BaseTableEntity
    {
        public TableTransportTask()
        {

        }
        public TableTransportTask(Dictionary<string,string> data)
        {
            setTable(data);
            setVectors(data);
        }

        [ForeignKey("TransportTask")]
        public override int? UserId { get; set; }

        public string AVector { get; set; }
        public string BVector { get; set; }
        
        public void setVectors(Dictionary<string,string> data)
        {
            List<double> avector = new List<double>(),
                        bvector = new List<double>();
            for(int i =0; i < NumRow; i++)
            {
                avector.Add(double.Parse(data[$"r{i+1}"]));
            }
            for (int i = 0; i < NumColumn; i++)
            {
                bvector.Add(double.Parse(data[$"fx{i+1}"]));
            }
            AVector = String.Join(sep, avector);
            BVector = String.Join(sep, bvector);
        }
        public List<double> GetAVector()
        {
            return AVector.Split(sep).Select(x=>double.Parse(x)).ToList();
        }
        public List<double> GetBVector()
        {
            return BVector.Split(sep).Select(x => double.Parse(x)).ToList();
        }
        
        public static bool operator ==(TableTransportTask This, TableTransportTask Other)
        {
            if(object.ReferenceEquals(This, null))
            {
                if (object.ReferenceEquals(Other, null))
                    return true;
                return false;
            }else if(object.ReferenceEquals(Other, null))
            {
                return false;
            }
            //return This.Table == Other.Table &&
            //    This.AVector == Other.AVector &&
            //    This.BVector == Other.BVector &&
            //    This.NumRow == Other.NumRow &&
            //    This.NumColumn == Other.NumColumn;
            return This == Other &&
                This.AVector == Other.AVector &&
                This.BVector == Other.BVector;
        }
        public static bool operator !=(TableTransportTask This, TableTransportTask Other)
        {
            return !(This == Other);
        }
    }
}
