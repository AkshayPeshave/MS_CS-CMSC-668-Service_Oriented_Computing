
set -- GSC*
#declare $count = echo "$*" | wc -w
#declare $fileList = ""

for i in $*
do
	curl 'http://130.85.254.232:8080/solr/update?fieldnames=longGscID,dblRightAscension,dblDeclension,fltPosError,fltMagnitude,fltMagnitudeError,intBand,intClass,strPlateNum,intMultipleObjects&commit=true' --data-binary @"${i}" -H 'Content-type:text/csv; charset=utf-8' -#
done
