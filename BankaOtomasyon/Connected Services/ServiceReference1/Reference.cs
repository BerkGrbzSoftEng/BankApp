﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankaOtomasyon.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.WebService1Soap")]
    public interface WebService1Soap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/hgsKontrol", ReplyAction="*")]
        bool hgsKontrol(int hgsMusteriNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/hgsKontrol", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> hgsKontrolAsync(int hgsMusteriNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/satınAl", ReplyAction="*")]
        int satınAl(long musteriTC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/satınAl", ReplyAction="*")]
        System.Threading.Tasks.Task<int> satınAlAsync(long musteriTC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BakiyeYukle", ReplyAction="*")]
        bool BakiyeYukle(decimal bakiye, int hgsNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BakiyeYukle", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> BakiyeYukleAsync(decimal bakiye, int hgsNo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebService1SoapChannel : BankaOtomasyon.ServiceReference1.WebService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebService1SoapClient : System.ServiceModel.ClientBase<BankaOtomasyon.ServiceReference1.WebService1Soap>, BankaOtomasyon.ServiceReference1.WebService1Soap {
        
        public WebService1SoapClient() {
        }
        
        public WebService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool hgsKontrol(int hgsMusteriNo) {
            return base.Channel.hgsKontrol(hgsMusteriNo);
        }
        
        public System.Threading.Tasks.Task<bool> hgsKontrolAsync(int hgsMusteriNo) {
            return base.Channel.hgsKontrolAsync(hgsMusteriNo);
        }
        
        public int satınAl(long musteriTC) {
            return base.Channel.satınAl(musteriTC);
        }
        
        public System.Threading.Tasks.Task<int> satınAlAsync(long musteriTC) {
            return base.Channel.satınAlAsync(musteriTC);
        }
        
        public bool BakiyeYukle(decimal bakiye, int hgsNo) {
            return base.Channel.BakiyeYukle(bakiye, hgsNo);
        }
        
        public System.Threading.Tasks.Task<bool> BakiyeYukleAsync(decimal bakiye, int hgsNo) {
            return base.Channel.BakiyeYukleAsync(bakiye, hgsNo);
        }
    }
}
