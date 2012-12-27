#include <stdio.h>
#include <string.h>

int read(int argc, char **argv)
{
  char *metafilename, *key_id;
  FILE *metafile;

  char string[1024];
  char seps[]="\n\t ";
  char *key_value;

  if(argc != 3) 
    {
      return 1;
    }
  metafilename=argv[1];
  key_id=argv[2];

  metafile = fopen(metafilename, "r");
  if(metafile==NULL) {
    return 1;
  }
  
  while(fgets(string, sizeof(string)-1, metafile)!=NULL) {
    key_value = strtok(string, seps);
    if (strcmp(key_value, key_id)==0)
      {
	key_value = strtok(NULL, seps);
	//printf("%s\n", key_value);
	return 0;
      }
  }
 
  fclose(metafile);
  return key_value;
}