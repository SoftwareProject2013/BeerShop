<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="BeerShop.Azure1" generation="1" functional="0" release="0" Id="d47954da-bc6a-4939-b95a-8f5143d26511" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="BeerShop.Azure1Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="BeerShop:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/LB:BeerShop:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="BeerShop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/MapBeerShop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BeerShopInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/MapBeerShopInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:BeerShop:Endpoint1">
          <toPorts>
            <inPortMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapBeerShop:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBeerShopInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BeerShop" generation="1" functional="0" release="0" software="D:\Beer\BeerShop\BeerShop.Azure1\csx\Release\roles\BeerShop" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BeerShop&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BeerShop&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopInstances" />
            <sCSPolicyUpdateDomainMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShopFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="BeerShopUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="BeerShopFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="BeerShopInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="83d3c42e-3236-468a-b1ab-b74d702bb939" ref="Microsoft.RedDog.Contract\ServiceContract\BeerShop.Azure1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="1733a492-717b-4f32-b24f-39df84df1ec5" ref="Microsoft.RedDog.Contract\Interface\BeerShop:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>