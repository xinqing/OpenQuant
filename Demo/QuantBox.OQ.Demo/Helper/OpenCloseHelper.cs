﻿using Newtonsoft.Json;
using OpenQuant.API;
using QuantBox.OQ.Extensions;
using QuantBox.OQ.Extensions.OrderText;

namespace QuantBox.OQ.Demo.Helper
{
    public class OpenCloseHelper
    {
        public static EnumOpenClose CheckOpenClose(Order order)
        {
            string text = order.Text;
            EnumOpenClose OpenClose = EnumOpenClose.OPEN;

            if (text.StartsWith("{") && text.EndsWith("}"))
            {
                TextParameter parameter = JsonConvert.DeserializeObject<TextParameter>(text);
                switch (parameter.Type)
                {
                    case EnumGroupType.COMMON:
                        {
                            TextCommon t = JsonConvert.DeserializeObject<TextCommon>(text);
                            OpenClose = t.OpenClose;
                        }
                        break;
                    case EnumGroupType.QUOTE:
                        {
                            TextQuote t = JsonConvert.DeserializeObject<TextQuote>(text);
                            OpenClose = t.OpenClose;
                        }
                        break;
                }
            }

            return OpenClose;
        }

        public static string GetOpenCloseString(EnumOpenClose oc)
        {
            switch (oc)
            {
                case EnumOpenClose.OPEN:
                    return TextCommon.Open;
                case EnumOpenClose.CLOSE:
                    return TextCommon.Close;
                case EnumOpenClose.CLOSE_TODAY:
                    return TextCommon.CloseToday;
                default:
                    return TextCommon.Open;
            }
        }

        public static PositionSide CheckLongShort(Order order,EnumOpenClose OpenClose)
        {
            if (EnumOpenClose.OPEN == OpenClose)
            {
                if (order.Side == OrderSide.Buy)
                {
                    return PositionSide.Long;
                }
            }
            else
            {
                if (order.Side == OrderSide.Buy)
                {
                }
                else
                {
                    return PositionSide.Long;
                }
            }
            return PositionSide.Short;
        }

        public static PositionSide CheckLongShort(Order order)
        {
            EnumOpenClose OpenClose = CheckOpenClose(order);
            return CheckLongShort(order, OpenClose);
        }
    }
}
