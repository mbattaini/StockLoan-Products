UPDATE tbInventoryFileDataMasks
   SET RecordLength = -1,
       HeaderFlag = 1,
       DataFlag = 2,
       TrailerFlag = 9,
       AccountLocale = 0,
       Delimiter = -1,
       AccountOrdinal = -1,
       SecIdOrdinal = -1,
       QuantityOrdinal = -1,
       RecordCountOrdinal = -1,
       AccountPosition = -1,
       AccountLength = -1,
       BizDateDD = 4,
       BizDateMM = 2,
       BizDateYY = 8,
       SecIdPosition = 4,
       SecIdLength = 9,
       QuantityPosition = 14,
       QuantityLength = 15,
       RecordCountPosition = 2,
       RecordCountLength = 9
 WHERE Desk = 'JPM.US.C'

