<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="BeerShop.Azure1" generation="1" functional="0" release="0" Id="4d0ea9f3-5afb-4f09-a58e-65fffa455c1c" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
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
          <role name="BeerShop" generation="1" functional="0" release="0" software="D:\Beer\BeerShop\BeerShop.Azure1\csx\Debug\roles\BeerShop" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
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
    <implementation Id="25a05cfd-0c31-4ccd-9f72-6a0aac5fa4ff" ref="Microsoft.RedDog.Contract\ServiceContract\BeerShop.Azure1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="c6cde54f-ec38-4f04-8c56-6e3a1f2502d9" ref="Microsoft.RedDog.Contract\Interface\BeerShop:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BeerShop.Azure1/BeerShop.Azure1Group/BeerShop:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>