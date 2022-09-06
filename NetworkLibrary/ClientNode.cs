using System;
using System.Net.Sockets;

namespace UDPLibrary
{

    public class ClientNode : IEquatable<string>
    {
        public TcpClient tclient;
        public byte[] Tx, Rx;
        public string strId;

        

        public ClientNode(TcpClient _tclient, byte[] _tx, byte[] _rx, string _str)
        {
            tclient = _tclient;
            Tx = _tx;
            Rx = _rx;
            strId = _str;
        }

        

        bool IEquatable<string>.Equals(string other)
        {
            if (string.IsNullOrEmpty(other)) return false;

            if (tclient == null) return false;

            return strId.Equals(other);
        }

        public override string ToString()
        {
            return strId;
        }
    }
}
