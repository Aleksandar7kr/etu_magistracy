<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
    <xsl:template match="/">
        <H2>�������� ����</H2>
        <xsl:for-each
                select="INVENTORY/BOOK[BINDING='������ �������']"
                order-by="-AUTHOR/LASTNAME; -AUTHOR/FIRSTNAME">
            <SPAN STYLE="font-style:italic">�����: </SPAN>
            <xsl:value-of select="AUTHOR"/><BR />
            <SPAN STYLE="font-style:italic">������������: </SPAN>                         <xsl:value-of select="TITLE"/><BR />
            <SPAN STYLE="font-style:italic">��� ���������: </SPAN>
            <xsl:value-of select="BINDING"/><BR />
            <SPAN STYLE="font-style:italic">����� �������: </SPAN>
            <xsl:value-of select="PAGES"/><BR />
            <SPAN STYLE="font-style:italic">����: </SPAN>
            <xsl:value-of select="PRICE"/><P />
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
