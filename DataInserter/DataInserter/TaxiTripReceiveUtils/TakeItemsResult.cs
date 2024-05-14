namespace DataInserter.TaxiTripReceiveUtils
{
    public struct TakeItemsResult<T>
    {
        public TakeItemsResult(List<T> result, bool hasNext)
        {
            Result = result;
            HasNext = hasNext;
        }

        public List<T> Result { get; set; }

        public bool HasNext { get; set; }
    }
}
