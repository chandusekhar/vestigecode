<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:template match ="CO">
    <Document font="Arial 9 Regular" padding="0 0 0 0" margin="10 10 10 10" brush="solid #ff000000" width="800" height="600">
      <LayoutSection font="Arial 36 Bold" padding="" brush="">
        <FrontLayout font="" padding="" brush="">
          <!--<xsl:choose>
            <xsl:when test="Status = 3">
              <Stamp font="" brush="solid #aaff4d4d" angle="315">Void</Stamp>
            </xsl:when>
            <xsl:when test="Status = 4">
              <Stamp font="" brush="solid #aaff4d4d" angle="315">Cancel</Stamp>
            </xsl:when>
            <xsl:when test="Status = 5">
              <Stamp font="" brush="solid #aaff4d4d" angle="315">Modified</Stamp>
            </xsl:when>
            <xsl:otherwise></xsl:otherwise>
          </xsl:choose>-->
        </FrontLayout>
        <BackLayout font="" padding="" brush=""></BackLayout>
      </LayoutSection>
      <DataSection font="" padding="" brush="">

        <!--This will contain the Vestige Logo, Address etc repeated on Every Page of Invoice-->

        <DocHeader font="" padding="" brush="">
          <Row halign="Center" valign="" height="" font="Arial 9 Bold" padding="" brush="">
            <Cell halign="" valign="" width="" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush=""></Text>
            </Cell>
            <Cell halign="" valign="" width="" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush=""></Text>
            </Cell>
            <Cell halign="" valign="" width="" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush=""></Text>
            </Cell>
          </Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="" colspan="" rowspan="" font="" padding="" brush="">
              <Line orientation="Horizontal" thickness="1" brush="" />
            </Cell>
          </Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
        </DocHeader>

        <!--Order No, Order Date, Order Taken By-->
        <DataHeader font="" padding="" brush="">
          <!--Customer Order No and Customer Order Date -->
          <Row halign="Center" valign="" height="" font="Arial 9 Bold" padding="" brush="">
            <Cell halign="Left" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush="">Order No: </Text>
            </Cell>
            <Cell halign="Left" valign="" width="71" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush="">
                <xsl:value-of select="CustomerOrderNo"/>
              </Text>
            </Cell>
            <Cell halign="Left" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush="">Order Date: </Text>
            </Cell>
            <Cell halign="Left" valign="" width="50" colspan="" rowspan="" font="" padding="" brush="">
              <Text font="" brush="">
                <xsl:value-of select="DisplayOrderDate"/>
              </Text>
            </Cell>
          </Row>

          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>

          <!-- This will contain Distributor No, Distributor Address, Delivery Address -->

          <Row halign="Center" valign="" height="" font="Arial 9 Bold" padding="" brush="">
            <Cell halign="Left" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Distributor Number: </Text>
            </Cell>
            <Cell halign="Left" valign="" width="66" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select ="DistributorId"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="60" colspan="2" rowspan="" font="" padding="" brush="">
              <Text>Delivery Address:</Text>
            </Cell>
          </Row>
          <Row halign="Left" valign="" height="" font="Arial 9 Bold" padding="" brush="">
            <Cell halign="" valign="" width="81" colspan="2" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="DistributorAddress"/>
              </Text>
            </Cell>
            <Cell halign="Right" valign="" width="60" colspan="2" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="DeliverToAddress"/>
              </Text>
            </Cell>
          </Row>
        </DataHeader>

        <!--This contains order details (Items purchased) and summation-->

        <DataDetail font="" padding="" brush="">
          <xsl:choose>
            <xsl:when test="count(CODetailList/CODetail) &gt; 0">

              <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
              <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
              <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
              <Row halign="" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="" colspan="" rowspan="" font="" padding="" brush="">
                  <Line orientation="Horizontal" thickness="1" brush="" />
                </Cell>
              </Row>
              
              <!--Item Details Header-->
              <Row halign="Left" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Sr. No.</Text>
                </Cell>
                <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Product Code</Text>
                </Cell>
                <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Product Name</Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Unit Price</Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Qty</Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Value</Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Tax</Text>
                </Cell>
                <Cell halign="" valign="" width="20" colspan="" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Total Price</Text>
                </Cell>
              </Row>

              <Row halign="" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="" colspan="" rowspan="" font="" padding="" brush="">
                  <Line orientation="Horizontal" thickness="1" brush="" />
                </Cell>
              </Row>
              <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>

              <!--Item Details-->

              <xsl:for-each select="CODetailList/CODetail">
                <Row halign="Left" valign="" height="" font="" padding="" brush="">
                  <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RecordNo"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="ItemCode"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="ItemReceiptName"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RoundedUnitPrice"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RoundedQty"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RoundedAmount"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RoundedTaxAmount"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RoundedTotalAmount"/>
                    </Text>
                  </Cell>
                </Row>
              </xsl:for-each>

              <!-- Blank spaces and line-->
              <Row halign="" valign="" height="5" font="" padding="" brush="">
              </Row>
              <Row halign="" valign="" height="5" font="" padding="" brush="">
              </Row>
              <Row halign="" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="52" colspan="3" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="Left" valign="" width="48" colspan="5" rowspan="" font="" padding="" brush="">
                  <Line orientation="Horizontal" thickness="1" brush="" />
                </Cell>
              </Row>

              <!--Totals of the Columns to be displayed-->
              <Row halign="" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text>Total: </Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text>
                    <xsl:value-of select="./RoundedTotalQty"/>
                  </Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text>
                    <xsl:value-of select="./RoundedOrderAmount"/>
                  </Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text>
                    <xsl:value-of select="./RoundedTaxAmount"/>
                  </Text>
                </Cell>
                <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
                  <Text>
                    <xsl:value-of select="./RoundedTotalAmount"/>
                  </Text>
                </Cell>
              </Row>
            </xsl:when>
          </xsl:choose>

          <!-- Blank spaces and line-->
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="52" colspan="3" rowspan="" font="" padding="" brush="">
            </Cell>
            <Cell halign="Left" valign="" width="48" colspan="5" rowspan="" font="" padding="" brush="">
              <Line orientation="Horizontal" thickness="1" brush="" />
            </Cell>
          </Row>

          <!--Order Payments-->
          <xsl:choose>
            <xsl:when test="count(COPaymentList/COPayment) &gt; 0">
              <!--Payment Details Header-->
              <Row halign="Left" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="52" colspan="4" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="Left" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Payment Mode.</Text>
                </Cell>
                <Cell halign="Left" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
                  <Text font="" brush="">Amount</Text>
                </Cell>
              </Row>
              <xsl:for-each select="COPaymentList/COPayment">
                <Row halign="Left" valign="" height="" font="" padding="" brush="">
                  <Cell halign="" valign="" width="52" colspan="4" rowspan="" font="" padding="" brush="">
                  </Cell>
                  <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="ItemReceiptDisplay"/>
                    </Text>
                  </Cell>
                  <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
                    <Text font="Arial 8 Regular" brush="">
                      <xsl:value-of select="RoundedPaymentAmount"/>
                    </Text>
                  </Cell>
                </Row>
              </xsl:for-each>

              <!-- Blank spaces and line-->
              <Row halign="" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="52" colspan="3" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="Left" valign="" width="48" colspan="5" rowspan="" font="" padding="" brush="">
                  <Line orientation="Horizontal" thickness="1" brush="" />
                </Cell>
              </Row>

              <!--Totals of the Payment Columns to be displayed-->
              <Row halign="" valign="" height="" font="" padding="" brush="">
                <Cell halign="" valign="" width="52" colspan="4" rowspan="" font="" padding="" brush="">
                </Cell>
                <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
                  <Text font="Arial 8 Regular" brush="">Total Payments: </Text>
                </Cell>
                <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
                  <Text font="Arial 8 Regular" brush="">
                    <xsl:value-of select="RoundedPaymentAmount"/>
                  </Text>
                </Cell>
              </Row>
            </xsl:when>
          </xsl:choose>
        </DataDetail>

        <!--Grand Total and Tax Summary-->

        <DataFooter font="" padding="" brush="">
          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Point Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
              <Text>GPV: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Net Product's Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
          </Row>

          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Point Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
              <Text>GPV: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Net Product's Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
          </Row>

          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Point Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
              <Text>GPV: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Net Product's Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
          </Row>

          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="7" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Point Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="15" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="30" colspan="" rowspan="" font="" padding="" brush="">
              <Text>GPV: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
              </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>Net Product's Value: </Text>
            </Cell>
            <Cell halign="" valign="" width="10" colspan="" rowspan="" font="" padding="" brush="">
              <Text>
                <xsl:value-of select="./RoundedOrderAmount"/>
              </Text>
            </Cell>
          </Row>
        </DataFooter>

        <!-- This contains the document footer that will be repeated on every page of the invoice like Terms and Conditions-->
        <DocFooter font="" padding="" brush="">
          <!-- Blank spaces and line-->
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="Left" valign="" width="102" colspan="" rowspan="" font="" padding="" brush="">
              <Line orientation="Horizontal" thickness="1" brush="" />
            </Cell>
          </Row>
          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="55" colspan="4" rowspan="" font="Arial 9 Bold" padding="" brush="">
              <Text>Terms &amp; Conditions:</Text>
            </Cell>
            <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="Arial 9 Bold" padding="" brush="">
              <Text font="Arial 8 Regular" brush="">for Vestige Marketing Pvt. Ltd.</Text>
            </Cell>
            <Cell halign="" valign="" width="22" colspan="2" rowspan="" font="" padding="" brush="">
            </Cell>
          </Row>

          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>

          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="55" colspan="4" rowspan="" font="" padding="" brush="">
              <Text>Any inaccuracies in this bill must be notified immediately on this receipt</Text>
            </Cell>
            <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
              <Text font="Arial 8 Regular" brush=""></Text>
            </Cell>
            <Cell halign="" valign="" width="22" colspan="2" rowspan="" font="" padding="" brush="">
            </Cell>
          </Row>

          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>

          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="55" colspan="4" rowspan="" font="" padding="" brush="">
              <Text>Company is not responsible after the goods leave this premises</Text>
            </Cell>
            <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
              <Text font="Arial 8 Regular" brush=""></Text>
            </Cell>
            <Cell halign="" valign="" width="22" colspan="2" rowspan="" font="" padding="" brush="">
            </Cell>
          </Row>

          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>

          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="55" colspan="4" rowspan="" font="" padding="" brush="">
              <Text>All disputes are subject to jurisdiction of Delhi only</Text>
            </Cell>
            <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="" padding="" brush="">
              <Text font="Arial 8 Regular" brush=""></Text>
            </Cell>
            <Cell halign="" valign="" width="22" colspan="2" rowspan="" font="" padding="" brush="">
            </Cell>
          </Row>

          <Row halign="" valign="" height="5" font="" padding="" brush=""></Row>
          
          <Row halign="" valign="" height="" font="" padding="" brush="">
            <Cell halign="" valign="" width="55" colspan="4" rowspan="" font="" padding="" brush="">
              <Text></Text>
            </Cell>
            <Cell halign="" valign="" width="25" colspan="2" rowspan="" font="Arial 9 Bold" padding="" brush="">
              <Text font="Arial 8 Regular" brush="">Prepared By: </Text>
            </Cell>
            <Cell halign="" valign="" width="22" colspan="2" rowspan="" font="" padding="" brush="">
            </Cell>
          </Row>
        </DocFooter>
      </DataSection>
    </Document>
  </xsl:template>
</xsl:stylesheet>
