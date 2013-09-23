<?xml version="1.0" encoding="windows-1251"?>
<!-- XslDemo06.xsl -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
    <xsl:template match="/">
        <H2>����� �� ������</H2>
        <TABLE BORDER="1" CELLPADDING="5">
            <THEAD>
                <TH>������������</TH>
                <TH>�����</TH>
                <TH>��� ���������</TH>
                <TH>����� �������</TH>
                <TH>����</TH>
            </THEAD>
            <xsl:for-each select="INVENTORY/BOOK[@InStock='���']">
                <TR ALIGN="CENTER">
                    <TD>
                        <xsl:value-of select="TITLE"/> <BR/>
                        (IS<xsl:value-of select="TITLE/@ISBN"/>)
                    </TD>
                    <TD>
                        <xsl:value-of select="AUTHOR"/> <BR/>
                    </TD>
                    <TD>
                        <xsl:value-of select="BINDING"/>
                    </TD>
                    <TD>
                        <xsl:value-of select="PAGES"/>
                    </TD>
                    <TD>
                        <xsl:value-of select="PRICE"/>
                    </TD>
                </TR>
            </xsl:for-each>
        </TABLE>
    </xsl:template>
</xsl:stylesheet>
